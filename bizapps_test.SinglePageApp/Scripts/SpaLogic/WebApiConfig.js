
var WebApiConfig = {};

(function () {

    var apiUrl = 'http://localhost:49401/api/';

    function getBlogByIdUrl(blogId) {
        var url = apiUrl + 'blog/getblogbyid/' + blogId;
        return url;
    };

    function getUserByNameUrl(userName) {
        var url = apiUrl + 'user/getuserbyname/' + userName;
        return url;
    };

    function createBlogUrl() {
        var url = apiUrl + 'blog/createblog';
        return url;
    };

    function deleteBlogUrl() {
        var url = apiUrl + 'blog/deleteblog';
        return url;
    };

    function updateBlogUrl() {
        var url = apiUrl + 'blog/updateblog';
        return url;
    };

    function getBlogByUseridUrl(userId) {
        var url = apiUrl + 'blog/getblogbyuserid/' + userId;
        return url;
    };

    function getPostByIdUrl(postId) {
        var url = apiUrl + 'post/getpost/' + postId;
        return url;
    };

    function getBlogCategoriesUrl(blogId) {
        var url = apiUrl + 'category/getblogcategories/' + blogId;
        return url;
    };

    function createCategoryUrl() {
        var url = apiUrl + 'category/createcategory';
        return url;
    };

    function updateCategoryUrl() {
        var url = apiUrl + 'category/updatecategory';
        return url;
    };

    function deleteCategoryUrl() {
        var url = apiUrl + 'category/deletecategory';
        return url;
    };

    function updatePostUrl() {
        var url = apiUrl + 'post/updatepost';
        return url;
    };

    function createPostUrl() {
        var url = apiUrl + 'post/createpost';
        return url;
    };

    function deletePostUrl() {
        var url = apiUrl + 'post/deletepost';
        return url;
    };

    function createCommentUrl() {
        var url = apiUrl + 'comment/createcomment';
        return url;
    };

    function updateCommentUrl() {
        var url = apiUrl + 'comment/updatecomment';
        return url;
    };

    function deleteCommentUrl() {
        var url = apiUrl + 'comment/deletecomment';
        return url;
    };

    WebApiConfig.GetBlogByIdUrl = getBlogByIdUrl;
    WebApiConfig.GetUserByNameUrl = getUserByNameUrl;
    WebApiConfig.CreateBlogUrl = createBlogUrl;
    WebApiConfig.DeleteBlogUrl = deleteBlogUrl;
    WebApiConfig.UpdateBlogUrl = updateBlogUrl;
    WebApiConfig.GetBlogByUseridUrl = getBlogByUseridUrl;
    WebApiConfig.GetPostByIdUrl = getPostByIdUrl;
    WebApiConfig.GetBlogCategoriesUrl = getBlogCategoriesUrl;
    WebApiConfig.CreateCategoryUrl = createCategoryUrl;
    WebApiConfig.UpdateCategoryUrl = updateCategoryUrl;
    WebApiConfig.DeleteCategoryUrl = deleteCategoryUrl;
    WebApiConfig.UpdatePostUrl = updatePostUrl;
    WebApiConfig.CreatePostUrl = createPostUrl;
    WebApiConfig.DeletePostUrl = deletePostUrl;
    WebApiConfig.CreateCommentUrl = createCommentUrl;
    WebApiConfig.UpdateCommentUrl = updateCommentUrl;
    WebApiConfig.DeleteCommentUrl = deleteCommentUrl;
})();



