import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Define your app routes here
const routes: Routes = [
  // { path: '', component: HomeComponent },
  // { path: 'about', component: AboutComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
