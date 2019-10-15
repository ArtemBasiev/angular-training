import { Component, OnInit } from '@angular/core';
import { Blog } from '../Models/Blog';
import { BlogService } from './blog.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from '../Models/Post';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

public blog: Blog = new Blog();
public filteredPosts = new Array<Post>();
public categoryId = 0;
private blogId: number | string;
pageNumber: number = 1;
pageSize: number = 3;

  constructor(
    private blogService: BlogService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.route.params.subscribe(param => {
      this.blogId = param['id'];
    });
   }

  ngOnInit() {

    this.blogService.GetBlogByID(this.blogId).subscribe( (data: Blog) => {
      this.blog = { ...data };
      if(this.blog.BlogPosts!=undefined || null){
        this.filteredPosts = this.blog.BlogPosts;
      }
      if(this.blog.Id == undefined || 0){
        this.router.navigate(['/404']);
       }});

  }

  filterByCategory(categoryId: number){
    if(this.categoryId == categoryId){
      this.categoryId = 0;
      this.filteredPosts = this.blog.BlogPosts;
    } else {
      this.categoryId = categoryId;
      this.filteredPosts =  this.blog.BlogPosts.filter( item => {
        let currentCategory = item.PostCategories.find(x=>x.Id==categoryId);
        if(currentCategory!=undefined){
          return item;
        }
      });
    }
  }

}
