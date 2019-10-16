import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserblogComponent } from './userblog.component';
import { UserblogRoutingModule } from './userblog-routing.module';
import { BlogService } from '../Services/blog/blog.service';
import { FormsModule } from '@angular/forms';
import { CreateblogComponent } from './createblog/createblog.component';
let UserblogModule = class UserblogModule {
};
UserblogModule = tslib_1.__decorate([
    NgModule({
        declarations: [UserblogComponent, CreateblogComponent],
        imports: [
            CommonModule,
            UserblogRoutingModule,
            FormsModule
        ],
        providers: [BlogService]
    })
], UserblogModule);
export { UserblogModule };
//# sourceMappingURL=userblog.module.js.map