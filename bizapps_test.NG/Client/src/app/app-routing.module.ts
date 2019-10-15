import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent }    from './page-not-found/page-not-found.component';


const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./post/post.module').then(mod => mod.PostModule)
  },
  {
    path: '',
    loadChildren: () => import('./blog/blog.module').then(mod => mod.BlogModule)
  },
  { path: 'Home/Index', redirectTo: '' },
  { path: '**', component: PageNotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
