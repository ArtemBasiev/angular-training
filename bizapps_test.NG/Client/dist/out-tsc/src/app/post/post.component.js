import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Post } from 'src/app/Models/Post';
let PostComponent = class PostComponent {
    constructor(postService, route, router) {
        this.postService = postService;
        this.route = route;
        this.router = router;
        this.post = new Post();
        this.route.params.subscribe(param => {
            this.postId = param['id'];
        });
    }
    ngOnInit() {
        this.postService.GetPostByID(this.postId).subscribe((data) => {
            this.post = Object.assign({}, data);
            if (this.post.Id == undefined || 0) {
                this.router.navigate(['/404']);
            }
        });
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