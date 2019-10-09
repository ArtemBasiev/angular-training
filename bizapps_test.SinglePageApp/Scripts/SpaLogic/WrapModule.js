

var WrapModule = {};

(function() {

    function showHidePager() {
        var pager = $(".pager");
        if (ViewModelsKO.FilteredPosts().length > ViewModelsKO.FilteredPosts.pageSize()) {
            $(pager).show();
        } else {
            $(pager).hide();
        };
    };


    function showPost(postId) {

        if ($('#postView')[0] === undefined) {
            ViewGetter.GetPostView();
        }
        ViewModelsSetter.GetPostById(postId);

    };

    function showPostList(blogId) {

        if ($('#divPostList')[0] === undefined) {
            ViewGetter.GetBlogView();
        }

        ViewModelsSetter.GetBlogById(blogId);

        showHidePager();
    }

    function showEditableBlog(userId) {

        if ($('#userblog')[0] === undefined) {
            ViewGetter.GetEditableBlogView();
        }

        ViewModelsSetter.GetUserBlog(userId);
    };

    WrapModule.ShowEditableBlog = showEditableBlog;
    WrapModule.ShowPostList = showPostList;
    WrapModule.ShowPost = showPost;
    WrapModule.ShowHidePager = showHidePager;

})();

