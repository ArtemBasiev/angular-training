


var z = document.getElementsByClassName(".button-answers");
z.innerHTML = "Answers(" + z.parent().find(".hideshowtriggered").getElementsByClassName(".comment-item").length+")";






function DisplayChildList(sender) {
    $(sender).next(".hideshowtriggered").toggle(function () {
        if ($(this).is(":visible")) {
            $(this).show();
            $(this).next(".button-showanswers").show();
            $(this).prev(".button-showanswers").hide();
        } else {
            $(this).hide();
            $(this).next(".button-showanswers").hide();
            $(this).prev(".button-showanswers").show();
        };
    });

};


function HideChildList(sender) {
    $(sender).prev(".hideshowtriggered").toggle(function () {
        if ($(this).is(":visible")) {
            $(this).show();
            $(this).prev(".button-showanswers").hide();
            $(this).next(".button-showanswers").show();
        } else {
            $(this).hide();
            $(this).prev(".button-showanswers").show();
            $(this).next(".button-showanswers").hide();
        };
    });

};


function displayEditComment(sender) {
    if ($(sender).is(".button-open-modal")) {
        $(sender).parent().prev("form").find(".divCommentEdit").toggle(function () {
            if ($(this).is(":visible")) {
                $(".divCommentEdit").hide();
                $(".CommentText").show();
                $(this).show();
                $(this).parent().prev(".CommentText").hide();
            } else {
                $(this).hide();
                $(this).parent().prev(".CommentText").show();
            };
        });

    } else {

        if ($(sender).is(".button-cancel")) {
            ($(sender).parent()).parent().toggle(function () {
                if ($(this).is(":visible")) {
                    $(this).prev(".CommentText").hide();
                    $(this).show();
                } else {
                    $(this).hide();
                    $(this).parent().prev(".CommentText").show();
                };
            });
        }
    }

};

function displayReplyComment(sender) {
    if ($(sender).is(".button-open-modal")) {
        ($(sender).parent()).parent().find(".divcommentreply").toggle(function () {
            if ($(this).is(":visible")) {
                $(".divcommentreply").hide();
                $(this).show();
            } else {
                $(this).hide();
            };
        });

    } else {

        if ($(sender).is(".button-cancel")) {
            ($(sender).parent()).parent().toggle(function () {
                if ($(this).is(":visible")) {
                    $(this).show();



                } else {
                    $(this).hide();
                };
            });
        }
    }

};

function hideValidator(sender) {

    $(sender).parent().toggle(function () {
        $(this).hide();

    });

};