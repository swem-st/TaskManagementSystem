# TaskManagementSystem  Software Challenge <br />
I represent you my proeject "TMS". <br />
PLEASE WHEN YOU CHECK THEM REMEMBER THAR I ONLY JUNIOR DEV AND WRITE IT ONLY FOR 3 DAYS. <br />
Also I try to do all your Requirements and I consider that it handled it just fine.  <br />
And project contain :  <br />
1. TMS contains information about tasks. Task can contain one or more subtasks.<br />
2. For every task following attributes are stored in TMS:<br />
  a. Name<br />
  b. Description<br />
  c. Start date<br />
  d. Finish date<br />
  e. State<br />
3. If task doesn’t have subtasks, then state of task is set when task is updated and can be one of the following<br />
  values: Planned, inProgress, Completed <br />
4. If task has subtasks, then its state is calculated by the following rules:   //////////     THIS THING IMPLEMENT WHEN YOU UPDATE SUBTASKS...  <br />
a. Completed, if all subtasks have state Completed<br />
b. inProgress, if there is at least one subtask with state inProgress<br />
c. Planned in all other cases<br />

