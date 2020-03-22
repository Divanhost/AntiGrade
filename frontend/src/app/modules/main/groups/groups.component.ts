import { Component, OnInit } from '@angular/core';
import { Group } from 'src/app/shared/models/group.model';
import { BaseComponent } from 'src/app/shared/classes';
import { GroupService } from 'src/app/core/services/group.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent extends BaseComponent implements OnInit {
  subjectId: number;
  groups: Group[] = [];
  constructor(private readonly groupService: GroupService,
              private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }
  ngOnInit(): void {
    this.getGroups();
  }

  getGroups() {
    if (!this.subjectId) {
      this.subscriptions.push(
        this.groupService.getGroups().subscribe((responce: ResponseModel<Group[]>) => {
          this.groups = responce.payload;
          console.log(this.groups);
        })
      );
    } else {
      this.subscriptions.push(
        this.groupService.getGroupsBySubjectId(this.subjectId).subscribe((responce: ResponseModel<Group[]>) => {
          this.groups = responce.payload;
          console.log(this.groups);
        })
      );
    }
  }
  addGroupToSubject() {

  }
  deleteGroup(groupId: number) {
    this.subscriptions.push(
      this.groupService.deleteGroup(groupId).subscribe((response: ResponseModel<boolean>) => {
        if (response.payload) {
            this.groups = this.groups.filter(x => x.id !== groupId);
        }
      })
    );
  }

}
