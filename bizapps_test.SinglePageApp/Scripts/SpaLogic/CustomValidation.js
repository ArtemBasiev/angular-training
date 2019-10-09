

var CustomValidation = {};

(function() {

    function validate(inputElement, validationMessageElement) {

        var elementText = $(inputElement).val();

        elementText = elementText.replace(/\s+/g, '');

        if (elementText !== "") {

            $(validationMessageElement).hide();
            return true;

        } else {

            $(validationMessageElement).show();
            return false;
        }

    };

    function validateCategoryEdit(event) {

        var sender = event.target;

        var spanCategoryErrorMessage = $(sender).parent().find('.category-validation-message');

        validate(sender, spanCategoryErrorMessage);

    };

    function validateCommentEditing(sender) {

        var commentEditingValidator = $(sender).next('.commentEditingValidator');

        validate(sender, commentEditingValidator);

    };


    CustomValidation.Validate = validate;
    CustomValidation.ValidateCategoryEdit = validateCategoryEdit;
    CustomValidation.ValidateCommentEditing = validateCommentEditing;

})();