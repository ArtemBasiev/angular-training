import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { User } from '../Models/User';
let UserAdapter = class UserAdapter {
    adapt(item) {
        if (item == null)
            return null;
        var user = new User();
        user.Id = item.Id;
        user.UserName = item.UserName;
        user.UserPassword = item.UserPassword;
        return user;
    }
};
UserAdapter = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], UserAdapter);
export { UserAdapter };
//# sourceMappingURL=UserAdapter.js.map