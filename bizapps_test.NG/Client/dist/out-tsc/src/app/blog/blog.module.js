import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogRoutingModule } from './blog-routing.module';
import { BlogComponent } from './blog.component';
import { BlogService } from './blog.service';
import { NgxPaginationModule } from 'ngx-pagination';
let BlogModule = class BlogModule {
};
BlogModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            BlogComponent
        ],
        imports: [
            CommonModule,
            BlogRoutingModule,
            NgxPaginationModule
        ],
        providers: [BlogService]
    })
], BlogModule);
export { BlogModule };
//# sourceMappingURL=blog.module.js.map