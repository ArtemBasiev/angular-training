import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PostComponent } from './post.component';
const routes = [
    { path: 'post/:id', redirectTo: '/blogpost/:id' },
    { path: 'blogpost/:id', component: PostComponent }
];
let PostRoutingModule = class PostRoutingModule {
};
PostRoutingModule = tslib_1.__decorate([
    NgModule({
        imports: [RouterModule.forChild(routes)],
        exports: [RouterModule]
    })
], PostRoutingModule);
export { PostRoutingModule };
//# sourceMappingURL=post-routing.module.js.map