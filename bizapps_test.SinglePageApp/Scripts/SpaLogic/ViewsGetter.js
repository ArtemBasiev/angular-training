
var ViewGetter = {};

(function() {

    function _bindValidationEventsToComments() {

        $('#areaCommentText').change(function () {

            var mainCommentValidator = $('#mainCommentValidator');

            CustomValidation.Validate(this, mainCommentValidator);

        });

    };


    function getPostView() {

        try {

            $.ajax({
                type: 'GET',
                url: '/Home/Post',
                dataType: 'html',
                async: false,
                success: function (data) {

                    $('#wrap').html(data);

                    ko.applyBindings(ViewModelsKO.PostViewModel, $('#postView')[0]);

                    _bindValidationEventsToComments();
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };

    function getBlogView() {

        try {

            $.ajax({
                type: 'GET',
                url: '/Home/Blog',
                dataType: 'html',
                async: true,
                success: function (data) {

                    $('#wrap').html(data);

                    ko.applyBindings(ViewModelsKO.FilteredPosts, $("#divPostList")[0]);
                    $(".blogBindable").each(function () {
                        ko.applyBindings(ViewModelsKO.BlogViewModel, $(this).get(0));
                    });
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };


    function _bindValidationEventsToEditableBlogView() {


        $('#textCategoryName').change(function () {


            var spanCategoryErrorMessage = $('#spanCategoryErrorMessage');

            CustomValidation.Validate(this, spanCategoryErrorMessage);

        });

        $('#textPostTitle').change(function () {


            var postTitleValidationMessage = $('#postTitleValidationMessage');

            CustomValidation.Validate(this, postTitleValidationMessage);

        });

        $('#textPostContent').change(function () {


            var contentValidationMessage = $('#contentValidationMessage');

            CustomValidation.Validate(this, contentValidationMessage);

        });

        $('#textEditableBlogTitle').change(function () {


            var blogTitleValidationMessage = $('#blogTitleValidationMessage');

            CustomValidation.Validate(this, blogTitleValidationMessage);

        });

        $('#textBlogTitle').change(function () {


            var blogcreationValidationMessage = $('#blogcreationValidationMessage');

            CustomValidation.Validate(this, blogcreationValidationMessage);

        });

    };



    function getEditableBlogView() {

        try {

            $.ajax({
                type: 'GET',
                url: '/Home/UserBlog',
                dataType: 'html',
                async: false,
                success: function (data) {

                    $('#wrap').html(data);

                    ko.applyBindings(ViewModelsKO.EditableBlogViewModel, $("#userblog")[0]);

                    _bindValidationEventsToEditableBlogView();
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };


    function _bindValidationEventsToRegistrationView() {

        $('#textUserName').change(function () {


            var userNameValidationMessage = $('#userNameValidationMessage');

            CustomValidation.Validate(this, userNameValidationMessage);

        });

        $('#textPassword').change(function () {


            var passwordValidationMessage = $('#passwordValidationMessage');

            CustomValidation.Validate(this, passwordValidationMessage);

        });

    };

    function getRegistrationView() {

        try {

            $.ajax({
                type: 'GET',
                url: '/Home/Registration',
                dataType: 'html',
                async: true,
                success: function (data) {

                    $('#wrap').html(data);

                    _bindValidationEventsToRegistrationView();
                }
            });

        } catch (exc) {

            console.log(exc);

        };

    };

    ViewGetter.GetRegistrationView = getRegistrationView;
    ViewGetter.GetEditableBlogView = getEditableBlogView;
    ViewGetter.GetBlogView = getBlogView;
    ViewGetter.GetPostView = getPostView;

})();


