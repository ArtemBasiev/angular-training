
var BlogCRUD = {};

(function () {

    function createBlog() {

        try {

            var textBlogTitle = $('#textBlogTitle');
            var blogcreationValidationMessage = $('#blogcreationValidationMessage');

            var dataIsValid = CustomValidation.Validate(textBlogTitle, blogcreationValidationMessage);

            if (dataIsValid) {


                var bearerToken = AuthorizationModule.GetTokenForSending();

                var userId = $('#userid').val();
                var blogEntity =
                {
                    BlogTitle: $('#textBlogTitle').val(),
                    CreatedBy: {
                        Id: userId
                    }
                };

                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.CreateBlogUrl(),
                    data: blogEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            WrapModule.ShowEditableBlog(userId);
                            $('#divCreateBlog').hide();
                            $('#divEditBlog').show();
                        } else {
                            $('#blogcreation-message').text("Something pass wrong");
                        }
                    },
                    fail: function () {
                        $('#blogcreation-message').text("Something pass wrong");
                    }
                });

            };

        } catch (exc) {

            console.log(exc);

        };

    };

    function saveBlogChanges() {

        try {

            var textEditableBlogTitle = $('#textEditableBlogTitle');
            var blogTitleValidationMessage = $('#blogTitleValidationMessage');
            var dataIsValid = CustomValidation.Validate(textEditableBlogTitle, blogTitleValidationMessage);

            if (dataIsValid) {

                var bearerToken = AuthorizationModule.GetTokenForSending();

                var blogEntity =
                {
                    Id: $('#blogid').val(),
                    BlogTitle: $('#textEditableBlogTitle').val()
                };

                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.UpdateBlogUrl(),
                    data: blogEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            $("#spanBlogTitle").text($('#textEditableBlogTitle').val());
                            enableBlogEditing();
                        };
                    }
                });

            };


        } catch (exc) {

            console.log(exc);

        };

    };

    function deleteBlog() {

        try {

            var bearerToken = AuthorizationModule.GetTokenForSending();

            var isConfirmed = window.confirm("Are you shure about to delete blog? You can't undo this action.");

            if (isConfirmed) {
                var blogEntity =
                {
                    Id: $('#blogid').val()
                };

                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.DeleteBlogUrl(),
                    data: blogEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            HashModule.RemoveHashFromUrl();
                            window.location.hash = 'edit';
                        };
                    },
                    fail: function () {

                    }
                });
            };

        } catch (exc) {

            console.log(exc);

        };

    };

    function enableBlogEditing() {

        try {

            $('#blogTitleValidationMessage').hide();

            var textBlogTitle = $('#textEditableBlogTitle');
            if (textBlogTitle.is(":visible")) {
                textBlogTitle.hide();
            } else {
                textBlogTitle.show();
                textBlogTitle.focus();
            };

            var btnSave = $('#btnSaveBlog');
            if (btnSave.is(":visible")) {
                btnSave.hide();
            } else {
                btnSave.show();
            };

            var spanBlogTitle = $('#spanBlogTitle');
            if (spanBlogTitle.is(":visible")) {
                spanBlogTitle.hide();
            } else {
                spanBlogTitle.show();
            };

        } catch (exc) {

            console.log(exc);

        };

    };

    BlogCRUD.CreateBlog = createBlog;
    BlogCRUD.SaveBlogChanges = saveBlogChanges;
    BlogCRUD.DeleteBlog = deleteBlog;
    BlogCRUD.EnableBlogEditing = enableBlogEditing;
})();

