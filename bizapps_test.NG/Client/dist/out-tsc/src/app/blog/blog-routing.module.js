import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BlogComponent } from './blog.component';
const routes = [
    { path: 'blog/:id', component: BlogComponent }
];
let BlogRoutingModule = class BlogRoutingModule {
};
BlogRoutingModule = tslib_1.__decorate([
    NgModule({
        imports: [RouterModule.forChild(routes)],
        exports: [RouterModule]
    })
], BlogRoutingModule);
export { BlogRoutingModule };
//# sourceMappingURL=blog-routing.module.js.map