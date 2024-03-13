import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from '../../environments/environments';

@Component({
  selector: 'app-rowing-data',
  templateUrl: './rowing-data.component.html',
  styleUrl: './rowing-data.component.css'
})
export class RowingDataComponent implements OnInit {
  rowingEventForm!: FormGroup;
  constructor(private http: HttpClient) { }

  formatTime(time: string): string {
    const parts = time.split(':');
    if (parts.length === 2) {
      return `00:${parts[0]}:${parts[1]}`;
    }
    return time;
  }

  ngOnInit() {
    this.rowingEventForm = new FormGroup({
      'distance': new FormControl(null, [Validators.required, Validators.min(1)]),
      'totalTime': new FormControl(null, Validators.required),
      'eventDate': new FormControl(null, Validators.required),
      'strokeRate': new FormControl(null, [Validators.required, Validators.min(1)])
    });
  }

  onSubmit() {
    console.log(this.rowingEventForm.value); // TODO remove this line
    const formData = this.rowingEventForm.value;
    formData.totalTime = this.formatTime(formData.totalTime);
    this.http.post(`${environment.apiUrl}/RowingEvent`, formData).subscribe(response => {
      console.log(response);
      // Here you can handle the response from the server
    }, error => {
      console.error(error);
      // Here you can handle errors
    });
  }
}
