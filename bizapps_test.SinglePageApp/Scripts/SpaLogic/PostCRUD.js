
var PostCRUD = {};

(function() {

    function createUpdatePost(sender) {

        try {

            var textPostTitle = $('#textPostTitle');

            var postTitleValidationMessage = $('#postTitleValidationMessage');

            var postTitleIsValid = CustomValidation.Validate(textPostTitle, postTitleValidationMessage);

            var textPostContent = $('#textPostContent');

            var contentValidationMessage = $('#contentValidationMessage');

            var postContentIsValid = CustomValidation.Validate(textPostContent, contentValidationMessage);

            if (postTitleIsValid && postContentIsValid) {

                var bearerToken = AuthorizationModule.GetTokenForSending();

                var postCategories = new Array();

                var blogCategories = $('.categoryCheckBox');

                for (var j = 0; j < blogCategories.length; j++) {
                    var checkboxBlogCategory = blogCategories[j];
                    if ($(checkboxBlogCategory).prop('checked') === true) {

                        var addedCategory = {
                            Id: $(checkboxBlogCategory).val()
                        };

                        postCategories.push(addedCategory);
                    };
                };

                if (postCategories.length < 1) {
                    postCategories = null;
                };


                var postEntity = null;
                var url = '';

                if ($(sender).attr('id') === 'buttonSavePostChanges') {
                    postEntity = {
                        Id: $('#hiddenCreatingPostId').val(),
                        PostTitle: $('#textPostTitle').val(),
                        PostContent: $('#textPostContent').val(),
                        PostCategories: postCategories
                    };

                    url = WebApiConfig.UpdatePostUrl();
                };

                if ($(sender).attr('id') === 'buttonCreatePost') {

                    postEntity = {
                        PostTitle: $('#textPostTitle').val(),
                        PostContent: $('#textPostContent').val(),
                        PostCategories: postCategories,
                        RelatedTo: {
                            Id: $('#blogid').val()
                        }
                    };

                    url = WebApiConfig.CreatePostUrl();
                };


                $.ajax({
                    type: 'POST',
                    url: url,
                    data: postEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            ViewModelsSetter.GetUserBlog($('#userid').val());
                            InterfaceActions.ShowTab('posts');
                        }
                    },
                    fail: function () {

                    }
                });

            };

           

        } catch (exc) {

            console.log(exc);

        };
 
    };


    function deletePost(sender) {

        try {

            var bearerToken = AuthorizationModule.GetTokenForSending();

            var postEntity = {
                Id: $(sender).parent().find('.hidden-postid').val()
            };

            var conformDeleting = window.confirm("Are you shure about to delete post? You can't undo this action.");
            if (conformDeleting) {
                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.DeletePostUrl(),
                    data: postEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            ViewModelsSetter.GetUserBlog($('#userid').val());
                        }
                    },
                    fail: function () {

                    }
                });
            };

        } catch (exc) {

            console.log(exc);

        };

        
    };


    function enablePostCreation(sender) {

        try {

            InterfaceActions.ShowTab($(sender).attr('data-toggle'));
            $('#textPostTitle').val('');
            $('#textPostContent').val('');
            $('.categoryCheckBox').prop('checked', false);
            $('#buttonSavePostChanges').hide();
            $('#buttonCreatePost').show();

        } catch (exc) {

            console.log(exc);

        };

    };

    function enablePostEditing(sender) {

        try {

            var editablePostId = $(sender).parent().find('.hidden-postid').val();
            ViewModelsSetter.GetPostById(editablePostId);

            InterfaceActions.ShowTab($(sender).attr('data-toggle'));

            $('#textPostTitle').val(ViewModelsKO.PostViewModel.PostTitle());
            $('#textPostContent').val(ViewModelsKO.PostViewModel.PostContent());
            $('#hiddenCreatingPostId').val(editablePostId);
            var blogCategories = $('.categoryCheckBox');
            var postCategories = ViewModelsKO.PostViewModel.PostCategories();
            blogCategories.prop('checked', false);

            for (var i = 0; i < postCategories.length; i++) {
                var postCategory = postCategories[i];
                for (var j = 0; j < blogCategories.length; j++) {
                    var checkboxBlogCategory = blogCategories[j];
                    if (postCategory.Id.toString() === $(checkboxBlogCategory).val().toString()) {
                        $(checkboxBlogCategory).prop('checked', true);
                    };
                };
            };
            $('#buttonSavePostChanges').show();
            $('#buttonCreatePost').hide();

        } catch (exc) {

            console.log(exc);

        };
       
    };

    PostCRUD.EnablePostEditing = enablePostEditing;
    PostCRUD.EnablePostCreation = enablePostCreation;
    PostCRUD.CreateUpdatePost = createUpdatePost;
    PostCRUD.DeletePost = deletePost;
})();

