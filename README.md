Readme.md

Backend:


Specialty:
 GET request - where All the specialties are listed in dropdown 

Doctor:
 GET:
/api/doctors - 
All doctors are listed based on specialty_id

/api/doctors/{id} -
Only doctor details are responded based on doctor_id

/api/doctors/{id}/slots-
List all the available slots related to the particular doctor based on doctor_id


Appointment:
POST:
/api/appointments-
To book the appointment based on 3 parameters slot_id, doctor_id, patient_id

PUT:
/api/appoinments/{id}/status-
To Update the status from pending to confirmed to completed 
 3 parameters new_status, changed_by, reason


Pateint:
GET:
/api/patients/{id}-
 To get the patient details based on patient_id

GET:
/api/patients/{id}/appointments -
List the information based on patient_id  
Which gives all the information about the appointment status from pending to confirmed to complete.

**** But Need to fix the completed one is getting repeated for more than 10 times in which is also effecting in database
—>Need to add the new appointments and do the testcases
Client side:

DoctorsSpecialties:
When a specialty is chosen then load doctors  based on filter specialties.
In each doctors grid  details with name, specialty, Available Slots (booking slots) navigates to '/booking/slots'

Available Slots:
/booking/slots where all the slots are available based on SlotAvailability Component for those will be displayed for that particular doctor
Select  the slot Available slot and routes to booking/confirm based on BookingConfirm where it displays all the Doctor,Specialty, Date, Time and Confirm Appointment.
Once the ConfirmAppointment it creates the Appointment.

Patient Details:
Once the ConfirmAppointment is created it routes to /dashboard  where I can see the Active Appointments and Past Appointments.
In ActiveAppointment we can the Pending status for the created appointment and Actions like Confirm , Cancel buttons.
Once the Confirm is clicked the status changes to Confirmed in status and Complete and Cancel button will be appeared in Actions.
Once the Complete button is clicked we can see the changes in Past Appointment which is completed

*** Need to check my Complete button in database which has more than 10times 
***Implement the Complete button clearly  

