import React, { useState, useEffect, Fragment } from 'react';
import './App.css';
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { IActivity } from '../models/activity'
import NavBar from '../../features/nav/NavBar'
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard'

const App = () => {
  const [activities, setActivities] = useState<IActivity[]>([])
  const [selectedActivity, SetSelectedActivity] = useState<IActivity | null>(null);
  const [EditMode, setEditMode] = useState(false);

  const hadleSelectActivity = (id: string) => {
    SetSelectedActivity(activities.filter(a => a.id === id)[0]);
    setEditMode(false);
  }

  const handleOpenCreateForm = () => {
    SetSelectedActivity(null);
    setEditMode(true);
  }
  const handleCreateActivity = (activity: IActivity) => {
    setActivities([...activities, activity])
    SetSelectedActivity(activity);
    setEditMode(false);
  }
  const handleEditActivity = (activity: IActivity) => {
    setActivities([...activities.filter(a => a.id !== activity.id), activity])
    SetSelectedActivity(activity);
    setEditMode(false);
  }

  const handleDeleteActivity = (id : string) => {
    setActivities([...activities.filter(a => a.id !== id)]);
  }
  useEffect(() => {
    axios.get<IActivity[]>('https://localhost:44381/api/activities')
      .then(response => {
        let activities: IActivity[] = [];
        response.data.forEach(item =>{
          item.date = item.date.split('.')[0];
          activities.push(item);
        })
        setActivities(activities);
      })
  }, [])
  return (
    <Fragment>
      <NavBar openCreateForm={handleOpenCreateForm}></NavBar>
      <Container style={{ marginTop: '7em' }}>
        <ActivityDashboard activities={activities}
          selectActivity={hadleSelectActivity}
          selectedActivity={selectedActivity!}
          editMode={EditMode}
          setEditMode={setEditMode}
          SetSelectedActivity={SetSelectedActivity}
          createActivity = {handleCreateActivity}
          editActivity = {handleEditActivity}
          deleteActivity = {handleDeleteActivity}/>
      </Container>
    </Fragment>
  );


}

export default App;
