

var HashModule = {};

(function() {

    function hashIsEquals(hash) {

        var hashLenght = hash.length;
        var currentHash = window.location.hash;
        var hashSubstr = currentHash.substring(1, parseInt(hashLenght) + 1);
        if (hashSubstr === hash) {
            return true;
        } else {
            return false;
        }
    };


    function recognizeHash() {

        $('#blogtitle').text('');
        var currentHash = window.location.hash;

        if (hashIsEquals('post')) {
            var postId = parseInt(currentHash.substring(5), 10);
            WrapModule.ShowPost(postId);
            InterfaceActions.SetUserInfoToComments();
        }

        if (hashIsEquals('blog')) {

            var blogId = parseInt(currentHash.substring(5), 10);
            WrapModule.ShowPostList(blogId);
        }

        if (hashIsEquals('reg')) {

            ViewGetter.GetRegistrationView();
        }

        if (hashIsEquals('edit')) {

            var userId = $('#userid').val();
            if (userId !== '') {
                WrapModule.ShowEditableBlog(userId);
            } else {
                removeHashFromUrl();
            }
        }

        if ((currentHash === "") || (currentHash === "#")) {
            $('#wrap').html('');
        }
    };

    function setPostHash(postId) {
        window.location.hash = "post" + postId;
    };

    function removeHashFromUrl() {
        window.location.hash = "";
        var noHashUrl = window.location.href.replace(/#.*$/, '');
        window.history.replaceState('', document.title, noHashUrl);
    }

    HashModule.RecognizeHash = recognizeHash;
    HashModule.SetPostHash = setPostHash;
    HashModule.RemoveHashFromUrl = removeHashFromUrl;
})();


var InterfaceActions = {};

(function() {
    function showTab(tabId) {

        var allTabs = $('.tab');
        $('.div-tab').hide();
        allTabs.removeClass('active');

        for (var i = 0; i < allTabs.length; i++) {
            var currentTab = allTabs[i];
            var dtValue = $(currentTab).find('a').attr('data-toggle').toString();
            if (dtValue === tabId.toString()) {

                $(currentTab).addClass('active');
            }
        };

        if (tabId.toString() === "editpost") {

            $('#postTitleValidationMessage').hide();
            $('#contentValidationMessage').hide();

        };

        $('#' + tabId).show();
    };

    function setUserInfoToComments() {
        var userName = CookieHandler.GetCookie('username');

        if (userName !== undefined) {
            $('#labelUserName').text(userName);
            $('#buttonPostComment').show();
            $('#buttonPostComment').text('Post as ' + userName);

            setCommentActions();

        } else {
            $('#labelUserName').text('Log In to comment');
            $('#buttonPostComment').hide();
        }
    };

    function setCommentActions() {

        try {

            var userName = CookieHandler.GetCookie('username');
            if (userName !== undefined) {

                $.ajax({
                    type: 'GET',
                    url: '/Home/CommentActions',
                    dataType: 'html',
                    async: false,
                    success: function (data) {

                        var commentItems = $('.comment-item');
                        for (var i = 0; i < commentItems.length; i++) {
                            var currentCommentItem = commentItems[i];
                            if ($(currentCommentItem).find('.CommentUserName').text() === userName) {
                                $(currentCommentItem).find('.commentActionsForAuthorized').html(data);

                            };
                        };
                    }
                });

            };

        } catch (exc) {

            console.log(exc);

        };

    };

    function openMenu() {
        document.getElementById("sidebar").classList.toggle('active');
    };

    function hideMainCommentValidator(sender) {

        $(sender).parent().hide();
    };

    InterfaceActions.OpenMenu = openMenu;
    InterfaceActions.SetCommentActions = setCommentActions;
    InterfaceActions.SetUserInfoToComments = setUserInfoToComments;
    InterfaceActions.ShowTab = showTab;
    InterfaceActions.HideMainCommentValidator = hideMainCommentValidator;

})();


function _bindValidationEventsToSideBar() {

    $('#username').change(function () {


        var sidebarLoginValMessage = $('#sidebarLoginValMessage');

        CustomValidation.Validate(this, sidebarLoginValMessage);

    });

    $('#password').change(function () {


        var sidebarPasswordValMessage = $('#sidebarPasswordValMessage');

        CustomValidation.Validate(this, sidebarPasswordValMessage);

    });
};



$(document).ready(function () {

    _bindValidationEventsToSideBar();
    AuthorizationModule.SetUserInfoToSidebar();
    HashModule.RecognizeHash();
});


window.onhashchange = function () {

    HashModule.RecognizeHash();
};

