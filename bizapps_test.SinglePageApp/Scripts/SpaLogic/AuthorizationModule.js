

var AuthorizationModule = {};

(function() {

    var tokenKey = 'tokenInfo';

    var authorizationServerUrl = "http://webapiblog/token";

    var registrationUrl = "http://webapiblog/api/user/createuser";

    function authorize(login, password) {

        var loginData = 'grant_type=password&username=' + login + '&password=' + password;

        var result = false;

        try {

            $.ajax({
                type: 'POST',
                url: authorizationServerUrl,
                data: loginData,
                async: false,
                success: function (data) {
                    CookieHandler.SetCookie(tokenKey, data.access_token, 360);
                    CookieHandler.SetCookie('username', login, 360);

                    result = true;
                },
                fail: function () {
                    result = false;
                }
            });

        } catch (exc) {

            console.log(exc);

        };

        return result;
    };



    function logOut() {
        CookieHandler.DeleteCookie(tokenKey);
        CookieHandler.DeleteCookie('username');
    };

    function register(user, successful, failed) {

        try {

            $.ajax({
                type: 'POST',
                url: registrationUrl,
                data: user,
                dataType: 'json',
                async: true,
                success: successful,
                fail: failed
            });

        } catch (exc) {

            console.log(exc);

        };

    };

    function getTokenForSending() {
        var token = CookieHandler.GetCookie(tokenKey);
        if (token === undefined) {
            HashModule.RemoveHashFromUrl();
        };

        var authorizationValue = 'Bearer ' + token;
        return authorizationValue;
    };



    function createUser() {

        try {

            var textUserName = $('#textUserName');
            var userNameValidationMessage = $('#userNameValidationMessage');
            var userNameIsValid = CustomValidation.Validate(textUserName, userNameValidationMessage);

            var textPassword = $('#textPassword');
            var passwordValidationMessage = $('#passwordValidationMessage');
            var passwordIsValid = CustomValidation.Validate(textPassword, passwordValidationMessage);

            if (userNameIsValid && passwordIsValid) {

                var userEntity = {
                    UserName: $('#textUserName').val(),
                    UserPassword: $('#textPassword').val()
                };
                register(userEntity, function () {
                    var isSuccessful = authorize(userEntity.UserName, userEntity.UserPassword);
                    if (isSuccessful) {
                        setUserInfoToSidebar();
                        HashModule.RemoveHashFromUrl();
                    }
                });

            };


        } catch (exc) {

            console.log(exc);

        };

        
    };

   

    function setUserInfoToSidebar() {
        var userName = CookieHandler.GetCookie('username');
        if (userName !== undefined) {

            $.ajax({
                type: 'GET',
                url: WebApiConfig.GetUserByNameUrl(userName),
                dataType: 'json',
                headers: {
                    'Authorization': getTokenForSending()
                },
                async: false,
                success: function (data) {

                    $('#menu-username').text(data.UserName);
                    $('#userid').val(data.Id);
                    $('#loginform').hide();
                    $('#logoutpanel').show();

                }
            });
        };
    };

    $('#logOut').click(function (e) {
        e.preventDefault();
        logOut();
        $('#loginform').show();
        $('#logoutpanel').hide();
        $('#menu-username').text('');
        $('#userid').val('');
        var currentHash = window.location.hash;
        HashModule.RemoveHashFromUrl();
        window.location.hash = currentHash;
    });

    $('#logIn').click(function (e) {
        e.preventDefault();

        var username = $('#username');
        var sidebarLoginValMessage = $('#sidebarLoginValMessage');
        var usernameIsValid = CustomValidation.Validate(username, sidebarLoginValMessage);

        var password = $('#password');
        var sidebarPasswordValMessage = $('#sidebarPasswordValMessage');
        var passwordIsValid = CustomValidation.Validate(password, sidebarPasswordValMessage);

        if (usernameIsValid && passwordIsValid) {

            var isSuccessful = authorize($(username).val(), $(password).val());
            if (isSuccessful) {

                $(sidebarLoginValMessage).hide();
                $(sidebarPasswordValMessage).hide();

                setUserInfoToSidebar();

                var currentHash = window.location.hash;
                var hashSubstrForPost = currentHash.substring(1, 5);
                if (hashSubstrForPost === "post") {

                    InterfaceActions.SetUserInfoToComments();
                };
            }

        };

        
    });

    AuthorizationModule.SetUserInfoToSidebar = setUserInfoToSidebar;
    AuthorizationModule.GetTokenForSending = getTokenForSending;
    AuthorizationModule.CreateUser = createUser;
    AuthorizationModule.SetUserInfoToSidebar = setUserInfoToSidebar;


})();



