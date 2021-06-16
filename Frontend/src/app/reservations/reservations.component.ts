import { Component, OnInit } from '@angular/core';
import { AddReservationModel } from '../models/AddReservationModel';
import { ApiService } from '../service/api.service';

export enum ReservationState {
  Free, Taken, Selected
}
@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {
  selectedDate: string = '';
  numbers: Array<number> = [];
  stateMap: any = {};
  loaded: boolean = false;
  constructor(private apiService: ApiService) {
    this.initialize();
  }

  ngOnInit(): void {

  }

  initialize() {
    this.numbers = Array(24).fill(0).map((x, i) => i);
    this.numbers.forEach(element => {
      this.stateMap[element] = ReservationState.Free;
    });
  }

  applyDate() {
    this.initialize();
    this.apiService.getReservations(this.selectedDate).subscribe(r => {
      r.body?.forEach(hour => {
        this.stateMap[hour] = ReservationState.Taken;
      });
      this.loaded = true;
    })
  }

  getClassFor(hour: number) {
    var state = this.stateMap[hour];
    if (state == ReservationState.Free) {
      return 'free';
    }
    if (state == ReservationState.Selected) {
      return 'selected';
    }
    if (state == ReservationState.Taken) {
      return 'taken';
    }
    return '';
  }

  timeSelected(hour: number) {
    var state = this.stateMap[hour];
    if (state == ReservationState.Taken) {
      return;
    }
    if (state == ReservationState.Free) {
      this.stateMap[hour] = ReservationState.Selected;
    }
    if (state == ReservationState.Selected) {
      this.stateMap[hour] = ReservationState.Free;
    }
  }



  reserve() {
    var props = Object.getOwnPropertyNames(this.stateMap);
    var selectedHours = props.filter(hour => this.stateMap[hour] == ReservationState.Selected).map(hour => Number(hour));
    var addModel = new AddReservationModel();
    addModel.date = this.selectedDate;
    addModel.hours = selectedHours;
    this.apiService.addReservation(addModel).subscribe(r => {
      this.initialize();
      this.applyDate();
    }

    );

    // this.apiService.getReservations().subscribe(r => {
    //   console.log(r.body);
    // })
  }

}
