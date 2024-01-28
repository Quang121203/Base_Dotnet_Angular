import { Component, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {
  slideNumber: number = 0;
  @ViewChild('listRef') list: ElementRef | undefined;
  distance: number = 0;

  handleClick = (direction: String) => {
    if (this.list) {
      console.log(this.list.nativeElement.offsetWidth);
      if (direction === "left" && this.slideNumber > 0) {
        this.slideNumber--;
        this.list.nativeElement.style.transform = `translateX(${230 + this.distance}px)`;
        this.distance += 230;
      }
      if (direction === "right" &&  this.slideNumber < 2) {
        this.slideNumber++;
        this.list.nativeElement.style.transform = `translateX(${-230 + this.distance}px)`;
        this.distance -= 230;
      }
    }
  }
}
