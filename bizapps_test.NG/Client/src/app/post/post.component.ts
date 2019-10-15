import { Component, OnInit } from '@angular/core';
import { PostService } from './post.service';
import { ActivatedRoute, Router } from '@angular/router';
import {Post} from 'src/app/Models/Post';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  post: Post = new Post();
  private postId: number | string;

  constructor(
    private postService: PostService,
    private route: ActivatedRoute,
    private router: Router
    ) {
      this.route.params.subscribe(param => {
        this.postId = param['id'];
      });
   }

  ngOnInit() {
    this.postService.GetPostByID(this.postId).subscribe( (data: Post) => {
    this.post = { ...data }
    if(this.post.Id == undefined || 0){
      this.router.navigate(['/404']);
     }});
  }

}
