import { Component } from '@angular/core';
import { ClientSearchComponent } from './clients/client-search/client-search.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'RoboVet 6 App Component';

  constructor( private matDialog: MatDialog) {}

  openDialog() {
    this.matDialog.open(ClientSearchComponent, {
            width: '600px',
            height: '650px'
        });
  }
}
