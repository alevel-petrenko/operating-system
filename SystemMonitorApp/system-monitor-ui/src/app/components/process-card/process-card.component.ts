import { Component, Input } from '@angular/core';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatCardModule} from '@angular/material/card';
import {MatChipsModule} from '@angular/material/chips';
import { ProcessInfo } from '../../models/ProcessInfo';

@Component({
  selector: 'app-process-card',
  standalone: true,
  imports: [MatCardModule, MatChipsModule, MatProgressBarModule],
  templateUrl: './process-card.component.html',
  styleUrl: './process-card.component.css'
})
export class ProcessCardComponent { 
  @Input()process!: ProcessInfo;
}
