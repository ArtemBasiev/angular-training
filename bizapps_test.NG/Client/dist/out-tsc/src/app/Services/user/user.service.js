import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';
let UserService = class UserService {
    constructor(http, adapter) {
        this.http = http;
        this.adapter = adapter;
    }
    GetUserByName(userName) {
        let url = config.apiUrl + config.getUserByNameUrl + userName;
        let blog = this.http.get(url).pipe(map(data => this.adapter.adapt(data)));
        return blog;
    }
};
UserService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], UserService);
export { UserService };
//# sourceMappingURL=user.service.js.map