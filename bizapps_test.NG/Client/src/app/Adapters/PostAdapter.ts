import { Injectable } from '@angular/core';
import { IAdapter } from './IAdapter';
import { Post } from 'src/app/Models/Post';


@Injectable({
    providedIn: 'root'
})
export class PostAdapter implements IAdapter<Post> {

  adapt(item: any): Post {
    if(item==null) return null;

    var post = new Post();
    post.Id = item.Id;
    post.PostContent = item.PostContent;
    post.PostTitle = item.PostTitle;
    post.PostCategories = item.PostCategories;
    post.CreationDate = new Date(item.CreationDate);
    return post;
  }
}