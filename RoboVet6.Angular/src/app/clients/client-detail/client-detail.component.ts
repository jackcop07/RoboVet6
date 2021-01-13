import { Component, OnInit, ViewChild } from '@angular/core';
import { IClient } from '../client';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ClientService } from '../client.service';
import {MatAccordion} from '@angular/material/expansion';

@Component({
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.css']
})
export class ClientDetailComponent implements OnInit {

  @ViewChild(MatAccordion) accordion: MatAccordion;
  pageTitle: string = 'Client Detail';
  client: IClient;
  constructor(private route: ActivatedRoute, private router: Router, private clientService: ClientService) { }

  ngOnInit() {
    let id = +this.route.snapshot.paramMap.get('id');

    this.pageTitle += `: ${id}`;

    this.clientService.getClient(id).subscribe({
      next: client => {
        this.client = client;
      }
    });
  }

  onBack(): void {
    this.router.navigate(['/clients']);

}

}
