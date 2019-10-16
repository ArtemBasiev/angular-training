import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
let MenuComponent = class MenuComponent {
    constructor() {
        this.sideBarIsOpened = false;
    }
    ngOnInit() {
    }
    toggleSideBar() {
        if (this.sideBarIsOpened) {
            this.sideBarIsOpened = false;
        }
        else {
            this.sideBarIsOpened = true;
        }
    }
};
MenuComponent = tslib_1.__decorate([
    Component({
        selector: 'app-menu',
        templateUrl: './menu.component.html',
        styleUrls: ['./menu.component.css']
    })
], MenuComponent);
export { MenuComponent };
//# sourceMappingURL=menu.component.js.map