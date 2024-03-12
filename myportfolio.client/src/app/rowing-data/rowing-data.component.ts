import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-rowing-data',
  templateUrl: './rowing-data.component.html',
  styleUrl: './rowing-data.component.css'
})
export class RowingDataComponent implements OnInit {
  rowingEventForm!: FormGroup;

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
  }
}
