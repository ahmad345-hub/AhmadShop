
using AhmadShop.BLL.Settings;
using AhmadShop.DAL.DTO.Requests;
using AhmadShop.DAL.DTO.Response;
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using Mapster;

using CategoryResponseDto = AhmadShop.DAL.DTO.Response.CategoryResponse;

namespace AhmadShop.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepositry<Category> _repo;

        public CategoryService(IGenericRepositry<Category> repo)
        {
            _repo = repo;
        }

        // Create
        public async Task<bool> CreateAsync(
            CategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            if (!AreTranslationValid(request.Translations))
                return false;

            var englishTranslation = request.Translations
                .First(t => t.Language.Equals(
                    SupportedLanguage.Default,
                    StringComparison.OrdinalIgnoreCase));

            if (!string.Equals(
                request.Name?.Trim(),
                englishTranslation.Name.Trim(),
                StringComparison.Ordinal))
                return false;

            var exists = await _repo.GetOneAsync(
                filter: c => c.Name == englishTranslation.Name.Trim(),
                cancellationToken: cancellationToken);

            if (exists is not null)
                return false;

            var category = new Category
            {
                Name = englishTranslation.Name.Trim(),
                Status = request.Status,

                Translations = request.Translations
                    .Select(t => new CategoryTranslation
                    {
                        Name = t.Name.Trim(),
                        Language = t.Language.Trim().ToLowerInvariant()
                    })
                    .ToList()
            };

            await _repo.CreateAsync(category, cancellationToken);

            return true;
        }

        // Get All
        public async Task<List<CategoryResponseDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var categories = await _repo.GetAsync(
                includes: new()
                {
                    c => c.Translations
                },
                cancellationToken: cancellationToken);

            return categories.Adapt<List<CategoryResponseDto>>();
        }

        // Get By Id
        public async Task<CategoryResponseDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var category = await _repo.GetOneAsync(
                filter: c => c.Id == id,
                includes: new()
                {
                    c => c.Translations
                },
                cancellationToken: cancellationToken);

            return category?.Adapt<CategoryResponseDto>();
        }

        // Update
        public async Task<bool> UpdateAsync(
            int id,
            CategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            if (!AreTranslationValid(request.Translations))
                return false;

            var englishTranslation = request.Translations
                .First(t => t.Language.Equals(
                    SupportedLanguage.Default,
                    StringComparison.OrdinalIgnoreCase));

            if (!string.Equals(
                request.Name?.Trim(),
                englishTranslation.Name.Trim(),
                StringComparison.Ordinal))
                return false;

            var category = await _repo.GetOneAsync(
                filter: c => c.Id == id,
                includes: new()
                {
                    c => c.Translations
                },
                tracked: true,
                cancellationToken: cancellationToken);

            if (category is null)
                return false;

            var exists = await _repo.GetOneAsync(
                filter: c =>
                    c.Id != id &&
                    c.Name == englishTranslation.Name.Trim(),
                cancellationToken: cancellationToken);

            if (exists is not null)
                return false;

            category.Name = englishTranslation.Name.Trim();
            category.Status = request.Status;

            foreach (var translationRequest in request.Translations)
            {
                var language = translationRequest.Language.Trim()
                    .ToLowerInvariant();

                var existingTranslation = category.Translations
                    .FirstOrDefault(t => t.Language == language);

                if (existingTranslation is not null)
                {
                    existingTranslation.Name = translationRequest.Name.Trim();
                }
                else
                {
                    category.Translations.Add(new CategoryTranslation
                    {
                        Name = translationRequest.Name.Trim(),
                        Language = language
                    });
                }
            }

            var requestedLanguages = request.Translations
                .Select(t => t.Language.Trim().ToLowerInvariant())
                .ToHashSet();

            var translationsToRemove = category.Translations
                .Where(t => !requestedLanguages.Contains(t.Language))
                .ToList();

            foreach (var translation in translationsToRemove)
            {
                category.Translations.Remove(translation);
            }

            await _repo.UpdateAsync(category, cancellationToken);

            return true;
        }

        // Delete
        public async Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var category = await _repo.FindAsync(
                id,
                cancellationToken);

            if (category is null)
                return false;

            await _repo.RemoveAsync(category, cancellationToken);

            return true;
        }

        // Validate Translations
        private static bool AreTranslationValid(
            List<CategoryTranslationRequest>? translations)
        {
            if (translations is null || translations.Count == 0)
                return false;

            if (translations.Any(t =>
                t is null ||
                string.IsNullOrWhiteSpace(t.Name) ||
                string.IsNullOrWhiteSpace(t.Language)))
                return false;

            var languages = translations
                .Select(t => t.Language.Trim())
                .ToList();

            if (languages.Distinct(
                StringComparer.OrdinalIgnoreCase).Count() != languages.Count)
                return false;

            return languages.Count == SupportedLanguage.Languages.Length
                && SupportedLanguage.Languages.All(language =>
                    languages.Contains(
                        language,
                        StringComparer.OrdinalIgnoreCase));
        }
    }
}
