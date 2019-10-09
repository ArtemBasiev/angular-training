

var CategoryCRUD = {};

(function() {

    function createCategory() {

        try {


            var textCategoryName = $('#textCategoryName');
            var spanCategoryErrorMessage = $('#spanCategoryErrorMessage');

            var dataIsValid = CustomValidation.Validate(textCategoryName, spanCategoryErrorMessage);

            if (dataIsValid) {

                var bearerToken = AuthorizationModule.GetTokenForSending();

                var blogId = $('#blogid').val();
                var categoryEntity = {
                    CategoryName: $(textCategoryName).val(),
                    RelatedTo: { Id: blogId }
                };


                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.CreateCategoryUrl(),
                    data: categoryEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            $('#textCategoryName').val('');
                            ViewModelsSetter.GetBlogCategories();
                        }
                    }
                });

            };


        } catch (exc) {

            console.log(exc);

        };

        
    };

    function saveCategoryChanges(sender) {

        try {

            var textCategoryEdit = $(sender).parent().find('.text-categoryedit');

            var spanCategoryErrorMessage = $(sender).parent().find('.category-validation-message');

            var dataIsValid = CustomValidation.Validate(textCategoryEdit, spanCategoryErrorMessage);

            if (dataIsValid) {

                var bearerToken = AuthorizationModule.GetTokenForSending();

                

                var categoryEntity = {
                    Id: $(sender).parent().find('.hidden-categoryid').val(),
                    CategoryName: textCategoryEdit.val()
                };

                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.UpdateCategoryUrl(),
                    data: categoryEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            ViewModelsSetter.GetBlogCategories();
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

    function deleteCategory(sender) {

        try {

        } catch (exc) {

            console.log(exc);

        };

        var bearerToken = AuthorizationModule.GetTokenForSending();

        var confirmDeleting = window.confirm("Are you shure about to delete category? You can't undo this action");
        if (confirmDeleting) {
            var categoryEntity = {
                Id: $(sender).parent().find('.hidden-categoryid').val()
            };

            $.ajax({
                type: 'POST',
                url: WebApiConfig.DeleteCategoryUrl(),
                data: categoryEntity,
                dataType: 'json',
                headers: {
                    'Authorization': bearerToken
                },
                async: true,
                success: function (data) {
                    if (data === true) {
                        ViewModelsSetter.GetBlogCategories();
                    }
                },
                fail: function () {

                }
            });
        };

    };

    function enableCategoryEditing(sender) {

        try {

            $('.category-validation-message').hide();

            var textCategoryEdit = $(sender).parent().find('.text-categoryedit');
            if (textCategoryEdit.is(":visible")) {
                textCategoryEdit.hide();
            } else {
                $('.text-categoryedit').hide();
                textCategoryEdit.show();
                textCategoryEdit.focus();
            };

            var btnSave = $(sender).parent().find('.btn-save');
            if (btnSave.is(":visible")) {
                btnSave.hide();
            } else {
                $('.btn-save').hide();
                btnSave.show();
            };

            var spanCategoryName = $(sender).parent().find('.span-categoryname');
            if (spanCategoryName.is(":visible")) {
                $('.span-categoryname').show();
                spanCategoryName.hide();
            } else {
                spanCategoryName.show();
            };

        } catch (exc) {

            console.log(exc);

        };

        
    };

    CategoryCRUD.CreateCategory = createCategory;
    CategoryCRUD.SaveCategoryChanges = saveCategoryChanges;
    CategoryCRUD.DeleteCategory = deleteCategory;
    CategoryCRUD.EnableCategoryEditing = enableCategoryEditing;

})();



