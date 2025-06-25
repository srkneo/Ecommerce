import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Breadcrumb } from 'xng-breadcrumb/lib/types/breadcrumb';

@Component({
  selector: 'app-header',  
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
    
  constructor(public bcService: BreadcrumbService) { 
    
  }
}
