import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import config from 'src/app/config/config.json';
let LoginformComponent = class LoginformComponent {
    constructor(authService, userService, http, cookieService, router, formBuilder) {
        this.authService = authService;
        this.userService = userService;
        this.http = http;
        this.cookieService = cookieService;
        this.router = router;
        this.formBuilder = formBuilder;
        this.isAuthorized = false;
        this.loginForm = this.formBuilder.group({
            username: '',
            password: ''
        });
    }
    ngOnInit() {
    }
    login(userLoginData) {
        let username = userLoginData.username;
        let password = userLoginData.password;
        let loginData = 'grant_type=password&username=' + username + '&password=' + password;
        let url = config.authorizationServerUrl;
        this.http.post(url, loginData).subscribe((resp) => {
            console.log(resp);
            let token = resp.access_token;
            if ((token != undefined) || (token != null)) {
                this.cookieService.set("token", token, 1);
                this.cookieService.set('user', username, 1);
                let userName = this.cookieService.get("user");
                this.userService.GetUserByName(userName).subscribe((data) => {
                    let user = Object.assign({}, data);
                    this.cookieService.set("userid", user.Id.toString(), 1);
                    this.isAuthorized = true;
                });
            }
        });
    }
    logout() {
        let result = this.authService.logout();
        if (result)
            this.isAuthorized = false;
        this.router.navigate(['']);
    }
};
LoginformComponent = tslib_1.__decorate([
    Component({
        selector: 'app-loginform',
        templateUrl: './loginform.component.html',
        styleUrls: ['./loginform.component.css']
    })
], LoginformComponent);
export { LoginformComponent };
//# sourceMappingURL=loginform.component.js.map