import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserblogComponent } from './userblog.component';
import { CreateblogComponent } from './createblog/createblog.component';
const routes = [
    { path: 'blog', component: UserblogComponent },
    { path: 'createblog', component: CreateblogComponent }
];
let UserblogRoutingModule = class UserblogRoutingModule {
};
UserblogRoutingModule = tslib_1.__decorate([
    NgModule({
        imports: [RouterModule.forChild(routes)],
        exports: [RouterModule]
    })
], UserblogRoutingModule);
export { UserblogRoutingModule };
//# sourceMappingURL=userblog-routing.module.js.map