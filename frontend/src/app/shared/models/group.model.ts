import { Student } from './student.model';
import { Course } from './course.model';

export class Group {
    id: number;
    name: string;
    students: Student[];
    course: Course;
}
