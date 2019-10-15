import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostRoutingModule } from './post-routing.module';
import { PostComponent } from './post.component';
import { PostService } from './post.service';
let PostModule = class PostModule {
};
PostModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            PostComponent
        ],
        imports: [
            CommonModule,
            PostRoutingModule
        ],
        providers: [PostService]
    })
], PostModule);
export { PostModule };
//# sourceMappingURL=post.module.js.map