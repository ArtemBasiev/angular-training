import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { BlogService } from 'src/app/Services/blog/blog.service';
import { Blog } from 'src/app/Models/Blog';
import { User } from 'src/app/Models/User';

@Component({
  selector: 'app-createblog',
  templateUrl: './createblog.component.html',
  styleUrls: ['./createblog.component.css']
})
export class CreateblogComponent implements OnInit {

  createBlogForm;

  constructor(
    private cookieService: CookieService,
    private router: Router,
    private formBuilder: FormBuilder,
    private blogService: BlogService
  ) { 
    this.createBlogForm = this.formBuilder.group({
      BlogTitle: ''
    });
  }

  ngOnInit() {

    let userId = this.cookieService.get("userid");
    this.blogService.GetBlogByUserID(userId).subscribe((result: Blog)=>{
      if(result!=null){
        this.router.navigate(["/blog"]);
      }
    })
  }

  createBlog(blogData){
    let blogTitle = blogData.BlogTitle;
    let userId = this.cookieService.get("userid");
    if(userId!=""){

      let blogDto = new Blog();
      blogDto.BlogTitle = blogTitle;
      let user = new User();
      user.Id = parseInt(userId, 10);
      blogDto.CreatedBy = user;
      this.blogService.CreateBlog(blogDto).subscribe((result: boolean)=>{
        if(result){
          this.router.navigate(["/blog"])
        }
      });

    } else {
      this.router.navigate([""]);
    }
  }

}
