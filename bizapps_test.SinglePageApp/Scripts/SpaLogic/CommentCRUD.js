
var CommentCRUD = {};

(function() {
    function createComment() {

        try {

            var areaCommentText = $('#areaCommentText');
            var mainCommentValidator = $('#mainCommentValidator');
            var commentTextIsValid = CustomValidation.Validate(areaCommentText, mainCommentValidator);

            if (commentTextIsValid) {

                var bearerToken = AuthorizationModule.GetTokenForSending();

                var postId = $('#postId').val();
                var commentEntity = {
                    CommentText: $('#areaCommentText').val(),
                    RelatedTo: { Id: postId },
                    CreatedBy: { Id: $('#userid').val() }
                };


                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.CreateCommentUrl(),
                    data: commentEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            $('#areaCommentText').val('');
                            ViewModelsSetter.GetPostById(postId);
                            InterfaceActions.SetCommentActions();
                        }
                    }
                });

            };

           

        } catch (exc) {

            console.log(exc);

        };
        
    };

    function deleteComment(sender) {

        try {

            var postId = $('#postId').val();

            var bearerToken = AuthorizationModule.GetTokenForSending();

            var confirmDeleting = window.confirm("Are you shure about to delete comment? You can't undo this action");
            if (confirmDeleting) {
                var commentEntity = {
                    Id: ($(sender).parent()).parent().find('.hidden-commentid').val()
                };

                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.DeleteCommentUrl(),
                    data: commentEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            ViewModelsSetter.GetPostById(postId);
                            InterfaceActions.SetCommentActions();
                        }
                    }
                });
            };

        } catch (exc) {

            console.log(exc);

        };
        

    };

    function enableCommentEditing(sender) {

        try {

            var commentEditingForm = $(sender).parent().find('.divCommentEdit');
            var commentText = ($(sender).parent()).parent().find('.CommentText');
            var areaEditCommentText = commentEditingForm.find('.areaEditCommentText');
            if (commentEditingForm.is(":visible")) {
                commentEditingForm.hide();
                areaEditCommentText.val('');
                commentText.show();

            } else {
                $('.divCommentEdit').hide();
                $('.CommentText').show();

                commentEditingForm.show();
                areaEditCommentText.val(commentText.text());
                areaEditCommentText.focus();
                commentText.hide();
            };

        } catch (exc) {

            console.log(exc);

        };
  
    };

    function cancelCommentEditing(sender) {

        try {

            var commentEditingForm = ($(sender).parent()).parent();
            var commentText = commentEditingForm.parent().parent().find('.CommentText');
            var areaEditCommentText = commentEditingForm.find('.areaEditCommentText');
            commentEditingForm.hide();
            areaEditCommentText.val('');
            ($(sender).parent()).parent().find('.commentEditingValidator').hide();
            commentText.show();

        } catch (exc) {

            console.log(exc);

        };

    };

    function saveCommentChanges(sender) {

        try {

            var areaEditCommentText = $(sender).parent().find('.areaEditCommentText');
            var commentEditingValidator = $(sender).parent().find('.commentEditingValidator');
            var commentTextIsValid = CustomValidation.Validate(areaEditCommentText, commentEditingValidator);

            if (commentTextIsValid) {

                var postId = $('#postId').val();

                var bearerToken = AuthorizationModule.GetTokenForSending();

                var textCommentEdit = $(sender).prev('.areaEditCommentText');

                var hiddenCommentId = (($(sender).parent()).parent()).parent().find('.hidden-commentid');

                var commentEntity = {
                    Id: hiddenCommentId.val(),
                    CommentText: textCommentEdit.val()
                };


                $.ajax({
                    type: 'POST',
                    url: WebApiConfig.UpdateCommentUrl(),
                    data: commentEntity,
                    dataType: 'json',
                    headers: {
                        'Authorization': bearerToken
                    },
                    async: true,
                    success: function (data) {
                        if (data === true) {
                            ViewModelsSetter.GetPostById(postId);
                            InterfaceActions.SetCommentActions();
                        }
                    }
                });

            };

        } catch (exc) {

            console.log(exc);

        };

        
    };

    CommentCRUD.CreateComment = createComment;
    CommentCRUD.SaveCommentChanges = saveCommentChanges;
    CommentCRUD.CancelCommentEditing = cancelCommentEditing;
    CommentCRUD.EnableCommentEditing = enableCommentEditing;
    CommentCRUD.DeleteComment = deleteComment;
})();

