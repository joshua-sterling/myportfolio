import { HttpClient, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from '../../environments/environments';
import { ToastrService } from 'ngx-toastr';
import { RowingEvent } from '../models/rowing-event';
import { RowingEventService } from './rowing-data.service'; 

@Component({
  selector: 'app-rowing-data',
  templateUrl: './rowing-data.component.html',
  styleUrl: './rowing-data.component.css'
})
export class RowingDataComponent implements OnInit {
  rowingEventForm!: FormGroup;
  rowingEvents: RowingEvent[] = [];
  isFormVisible = true;
  totalRecords = 0;
  currentPage = 1;
  recordsPerPage = 10;
  sortColumn = '';
  sortAscending = true;
  isLoading = false;
  data = [];
  constructor(private http: HttpClient, private toastr: ToastrService,
    private rowingEventService: RowingEventService) { }

  toggleForm() { 
    this.isFormVisible = !this.isFormVisible;
  }

  formatTime(time: string): string {
    const parts = time.split(':');
    if (parts.length === 2) {
      return `00:${parts[0]}:${parts[1]}`;
    }
    return time;
  }

  ngOnInit() {
    this.isFormVisible = false;
    this.rowingEventForm = new FormGroup({
      'distance': new FormControl(null, [Validators.required, Validators.min(1)]),
      'totalTime': new FormControl(null, Validators.required),
      'eventDate': new FormControl(null, Validators.required),
      'strokeRate': new FormControl(null, [Validators.required, Validators.min(1)])
    });

    this.getRowingEvents();
    this.getChartData();
  }

  changeRecordsPerPage() {
    this.currentPage = 1; //chaning the number of records to display, so go back to the first page
    this.getRowingEvents();
  }

  getRowingEvents() {
    const skip = (this.currentPage - 1) * this.recordsPerPage;
    const take = this.recordsPerPage;
    let loadingTimeout = setTimeout(() => {      
      this.isLoading = true;
    }, 1000); // Set isLoading to true after 1 second

    this.rowingEventService.getRowingEvents(skip,take,this.sortColumn,this.sortAscending).subscribe((response: any) => {
      clearTimeout(loadingTimeout);
      this.rowingEvents = response.data;
      this.totalRecords = response.total;
      this.isLoading = false;
    }, error => {
      clearTimeout(loadingTimeout);
      console.error(error);
    });
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getRowingEvents();
    }
  }

  nextPage() {
    if (this.currentPage < this.maxPage) {
      this.currentPage++;
      this.getRowingEvents();
    }
  }

  get maxPage() {
    return Math.ceil(this.totalRecords / this.recordsPerPage);
  }

  sort(column: string) {
    // If the user clicked the same column, toggle the sort direction
    if (this.sortColumn === column) {
      this.sortAscending = !this.sortAscending;
    } else {
      // If the user clicked a different column, sort in ascending order
      this.sortColumn = column;
      this.sortAscending = true;
    }

    // Fetch the data with the new sort column and direction
    this.getRowingEvents();
  }
  

  onSubmit() {    
    const formData = this.rowingEventForm.value;
    formData.totalTime = this.formatTime(formData.totalTime);
    this.rowingEventService.addRowingEvent( formData).subscribe((response: HttpResponse<any>) => {      
      if (response.status === 200) { 
        this.toastr.success('Form submitted successfully!');
        this.isFormVisible = false;
        this.rowingEventForm.reset();
        this.getRowingEvents();
        this.getChartData();
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

  getChartData() {
    this.rowingEventService.getChartData().subscribe((response: any) => {
      this.data = response;
    }, error => {
      console.error(error);
    });
  }

  formatYAxisTicks(value: number): string {
    return Math.floor(value).toString();
  }
}
