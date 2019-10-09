
var ViewModelsSetter = {};

(function() {

    function getPostById(postId) {

        try {

            $.ajax({
                type: 'GET',
                url: WebApiConfig.GetPostByIdUrl(postId),
                dataType: 'json',
                async: false,
                success: function (data) {

                    ViewModelsKO.PostViewModel.Id(data.Id);
                    ViewModelsKO.PostViewModel.PostTitle(data.PostTitle);
                    ViewModelsKO.PostViewModel.PostContent(data.PostContent);
                    ViewModelsKO.PostViewModel.CreationDate(data.CreationDate);
                    ViewModelsKO.PostViewModel.PostCategories.removeAll();
                    ViewModelsKO.PostViewModel.PostComments.removeAll();
                    for (var j = 0; j < data.PostCategories.length; j++) {
                        ViewModelsKO.PostViewModel.PostCategories.push(data.PostCategories[j]);
                    };

                    for (var i = 0; i < data.PostComments.length; i++) {
                        ViewModelsKO.PostViewModel.PostComments.push(data.PostComments[i]);
                    };
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };

    function getBlogById(blogId) {

        try {

            $.ajax({
                type: 'GET',
                url: WebApiConfig.GetBlogByIdUrl(blogId),
                dataType: 'json',
                async: false,
                success: function (data) {
                    ViewModelsKO.BlogViewModel.blogPosts.removeAll();
                    ViewModelsKO.BlogViewModel.blogCategories.removeAll();
                    ViewModelsKO.BlogViewModel.blogTitle(data.BlogTitle);
                    $('#blogtitle').text(data.BlogTitle);
                    for (var i = 0; i < data.BlogPosts.length; i++) {
                        ViewModelsKO.BlogViewModel.blogPosts.push(data.BlogPosts[i]);
                    }

                    for (var j = 0; j < data.BlogCategories.length; j++) {
                        ViewModelsKO.BlogViewModel.blogCategories.push(data.BlogCategories[j]);
                    }
                    ViewModelsKO.PushDataToFilteredPosts();
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };

    function getUserBlog(userId) {

        try {

            $.ajax({
                type: 'GET',
                url: WebApiConfig.GetBlogByUseridUrl(userId),
                dataType: 'json',
                async: true,
                success: function (data) {
                    if (data === null) {
                        $('#divEditBlog').hide();
                        $('#divCreateBlog').show();
                    } else {
                        ViewModelsKO.EditableBlogViewModel.blogPosts.removeAll();
                        ViewModelsKO.EditableBlogViewModel.blogCategories.removeAll();
                        ViewModelsKO.EditableBlogViewModel.blogTitle(data.BlogTitle);
                        ViewModelsKO.EditableBlogViewModel.blogId(data.Id);

                        for (var i = 0; i < data.BlogPosts.length; i++) {
                            ViewModelsKO.EditableBlogViewModel.blogPosts.push(data.BlogPosts[i]);
                        }

                        for (var j = 0; j < data.BlogCategories.length; j++) {
                            ViewModelsKO.EditableBlogViewModel.blogCategories.push(data.BlogCategories[j]);
                        }
                    };
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };

    function getBlogCategories() {

        try {

            var blogId = $('#blogid').val();

            $.ajax({
                type: 'GET',
                url: WebApiConfig.GetBlogCategoriesUrl(blogId),
                dataType: 'json',
                async: false,
                success: function (data) {
                    ViewModelsKO.EditableBlogViewModel.blogCategories.removeAll();

                    for (var j = 0; j < data.length; j++) {
                        ViewModelsKO.EditableBlogViewModel.blogCategories.push(data[j]);
                    }
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };

    ViewModelsSetter.GetBlogCategories = getBlogCategories;
    ViewModelsSetter.GetUserBlog = getUserBlog;
    ViewModelsSetter.GetBlogById = getBlogById;
    ViewModelsSetter.GetPostById = getPostById;

})();






