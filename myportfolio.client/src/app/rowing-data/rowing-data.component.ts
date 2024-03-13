import { HttpClient, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from '../../environments/environments';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-rowing-data',
  templateUrl: './rowing-data.component.html',
  styleUrl: './rowing-data.component.css'
})
export class RowingDataComponent implements OnInit {
  rowingEventForm!: FormGroup;
  constructor(private http: HttpClient, private toastr: ToastrService) { }

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
    this.http.post(`${environment.apiUrl}/RowingEvent`, formData,{ observe: 'response' }).subscribe((response: HttpResponse<any>) => {      
      if (response.status === 200) { // check the status code
        this.toastr.success('Form submitted successfully!');
        this.rowingEventForm.reset(); 
      }
    }, error => {
      if (error.status === 400 && error.error.errors) { //validation errors
        for (const key in error.error.errors) { // could be one per field submitted
          if (error.error.errors.hasOwnProperty(key)) {
            this.toastr.error(error.error.errors[key][0]); // display the first error message for each field
          }
        }
      } else {
        this.toastr.error('An error occurred while submitting the form.');
      }
    });
  }
}
