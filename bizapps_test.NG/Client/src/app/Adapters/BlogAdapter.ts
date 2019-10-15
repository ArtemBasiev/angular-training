import { Injectable } from '@angular/core';
import { IAdapter } from './IAdapter';
import { Blog } from 'src/app/Models/Blog';


@Injectable({
    providedIn: 'root'
})
export class BlogAdapter implements IAdapter<Blog> {

  adapt(item: any): Blog {
    if(item==null) return null;

    var blog = new Blog();
    blog.Id = item.Id;
    blog.BlogTitle = item.BlogTitle;
    blog.BlogCategories = item.BlogCategories;
    blog.BlogPosts = item.BlogPosts;
    blog.CreationDate = new Date(item.CreationDate);
    return blog;
  }
}