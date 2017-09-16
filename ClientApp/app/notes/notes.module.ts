import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SharedModule } from "../shared/shared.module";
import { UsersModule } from "../users/users.module";

import { AuthGuardService } from "../shared/guards/auth-guard.service";
import { TenantGuardService } from "../shared/guards/tenant-guard.service";
import { EventHubConnectionGuardService } from "../shared/guards/event-hub-connection-guard.service";
import { CurrentUserGuardService } from "../users/current-user-guard.service";

import { NotesService } from "./notes.service";

import { NoteEditComponent } from "./note-edit.component";
import { NoteEditPageComponent } from "./note-edit-page.component";
import { NoteListItemComponent } from "./note-list-item.component";
import { NotePaginatedListComponent } from "./note-paginated-list.component";
import { NotePaginatedListPageComponent } from "./note-paginated-list-page.component";
import { NotesLeftNavComponent } from "./notes-left-nav.component";

export const NOTE_ROUTES: Routes = [{
    path: 'notes',
    component: NotePaginatedListPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'notes/create',
    component: NoteEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'notes/:id',
    component: NoteEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
}];

const declarables = [
    NoteEditComponent,
    NoteEditPageComponent,
    NoteListItemComponent,
    NotePaginatedListComponent,
    NotePaginatedListPageComponent,
    NotesLeftNavComponent
];

const providers = [NotesService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, RouterModule.forChild(NOTE_ROUTES), SharedModule, UsersModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class NotesModule { }
