import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
const routes = [
    {
        path: '',
        loadChildren: () => import('./post/post.module').then(mod => mod.PostModule)
    },
    {
        path: '',
        loadChildren: () => import('./blog/blog.module').then(mod => mod.BlogModule)
    },
    {
        path: '',
        loadChildren: () => import('./userblog/userblog.module').then(mod => mod.UserblogModule)
    },
    { path: 'Home/Index', redirectTo: '' },
    { path: '**', component: PageNotFoundComponent }
];
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = tslib_1.__decorate([
    NgModule({
        imports: [RouterModule.forRoot(routes)],
        exports: [RouterModule]
    })
], AppRoutingModule);
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map