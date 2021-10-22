import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequest = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy() {
    this.busyRequest++;
    this.spinnerService.show(undefined, {
      type: 'square-jelly-box',
      bdColor: 'rgba(255, 255, 255,0)',
      color: '#007bff'
    });
  }

  idle() {
    this.busyRequest--;
    if(this.busyRequest <= 0) {
      this.busyRequest--;
      this.spinnerService.hide();
    }
  }
}
