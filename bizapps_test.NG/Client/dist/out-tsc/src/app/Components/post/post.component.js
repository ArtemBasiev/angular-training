import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
let PostComponent = class PostComponent {
    constructor(postService) {
        this.post = postService.GetPostByID(1);
    }
    ngOnInit() {
    }
};
PostComponent = tslib_1.__decorate([
    Component({
        selector: 'app-post',
        templateUrl: './post.component.html',
        styleUrls: ['./post.component.css']
    })
], PostComponent);
export { PostComponent };
//# sourceMappingURL=post.component.js.map