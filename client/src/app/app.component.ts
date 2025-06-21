import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'eShopping';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // Example of making an HTTP request
    this.http.get('http://localhost:8000/api/v1/Catalog/GetProductByProductName/Adidas%20FIFA%20World%20Cup%202018%20OMB%20Football').subscribe({

      next: response => console.log(response),
      error: error => console.error('There was an error!', error),
      complete: () => console.log('HTTP request completed')

    })
  }
}
