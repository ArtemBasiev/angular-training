import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import config from 'src/app/config/config.json';
let AuthService = class AuthService {
    constructor(cookieService, http) {
        this.cookieService = cookieService;
        this.http = http;
    }
    login(username, password) {
        let loginData = 'grant_type=password&username=' + username + '&password=' + password;
        let url = config.apiUrl + config.authorizationServerUrl;
        this.http.post(url, loginData).subscribe((resp) => {
            console.log(resp);
            let token = resp.access_token;
            if ((token != undefined) || (token != null)) {
                this.cookieService.set("token", token, 1);
                this.cookieService.set('user', username, 1);
            }
        });
    }
    logout() {
        this.cookieService.delete('user');
        this.cookieService.delete('token');
        this.cookieService.delete('userid');
        let userCookieValue = this.cookieService.get('user');
        let tokenCookieValue = this.cookieService.get('token');
        let useridCookieValue = this.cookieService.get('userid');
        if ((userCookieValue == "") && (tokenCookieValue == "") && (useridCookieValue == "")) {
            return true;
        }
        else {
            return false;
        }
    }
    GetAuthToken() {
        let token = this.cookieService.get("token");
        return "Bearer " + token;
    }
};
AuthService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], AuthService);
export { AuthService };
//# sourceMappingURL=auth.service.js.map