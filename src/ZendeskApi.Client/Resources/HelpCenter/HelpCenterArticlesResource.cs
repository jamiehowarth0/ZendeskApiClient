using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class HelpCenterArticlesResource : AbstractBaseResource<HelpCenterArticlesResource>,
        IHelpCenterArticlesResource
    {
        private const string ResourceUri = "api/v2/help_center";

        public HelpCenterArticlesResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "help_center/articles")
        { }

        public async Task<HelpCenterArticleListResponse> GetAllAsync(
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<HelpCenterArticleListResponse>(
                 locale == null ? $"{ResourceUri}/articles" : $"{ResourceUri}/{locale}/articles",
                "list-articles",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterArticleListResponse> GetAllByCategoryIdAsync(
            long categoryId,
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<HelpCenterArticleListResponse>(
                locale == null ? $"{ResourceUri}/categories/{categoryId}/articles" : $"{ResourceUri}/{locale}/categories/{categoryId}/articles",
                "list-articles",
                "GetAllByCategoryIdAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterArticleListResponse> GetAllBySectionIdAsync(
            long sectionId,
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<HelpCenterArticleListResponse>(
                locale == null ? $"{ResourceUri}/sections/{sectionId}/articles" : $"{ResourceUri}/{locale}/sections/{sectionId}/articles",
                "list-articles",
                "GetAllBySectionIdAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterArticleListResponse> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<HelpCenterArticleListResponse>(
                $"{ResourceUri}/users/{userId}/articles",
                "list-articles",
                "GetAllBySectionIdAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterArticle> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<SingleHelpCenterArticleResponse>(
                locale == null ? $"{ResourceUri}/articles/{id}" : $"{ResourceUri}/{locale}/articles/{id}",
                "show-article",
                $"GetAsync({locale}, {id})",
                $"Help center article {locale} {id} not found",
                cancellationToken: cancellationToken);

            return response?
                .Article;
        }
    }
}