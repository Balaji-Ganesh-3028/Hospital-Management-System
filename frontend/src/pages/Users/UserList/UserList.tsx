import React, { useEffect, useState } from 'react';
import './UserList.css';
import {  DeleteUesrProfile, GetAllUserProfile } from '../../../Service/User-Profile/user-profile';
import type { AllUserDetails } from '../../../Models/User-Profile';
import { useNavigate } from 'react-router-dom';
import { notify } from '../../../Service/Toast-Message/Toast-Message';
import { ToastMessageTypes } from '../../../Enums/Toast-Message';

interface UserListProps {
  onClickEdit: () => void
}
function UserList({ onClickEdit }: UserListProps) {
  const [userList, SetUserList] = useState<AllUserDetails[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    onload();
  }, []);

  const onload = async () => {
    const response = await GetAllUserProfile();
    SetUserList(response);
  }

  const navigateToUserProfile = (userId: number) => {
    console.log(userId);
    onClickEdit();
    navigate(`/users?page=userProfile&action=edit&userId=${userId}`)
  }

  async function deleteUser(userId: number): Promise<void> {
    const confirmed = window.confirm('Are you sure you want to delete this user?');
    if (!confirmed) return;

    try {
      // Attempt to delete on the server (adjust URL to match your API)
      const response = await DeleteUesrProfile(userId);
      
      if (response.includes("deleted successfully")) {
        notify(response, ToastMessageTypes.INFO);
        await onload();
      } else {
        notify(response, ToastMessageTypes.ERROR);
      }

      // Update local list optimistically
    } catch (err) {
      console.error('Failed to delete user', err);
      alert('Failed to delete user. Please try again.');
    }
  }
  return (
    <div className="user-list-container">
      <div className="user-list-actions">
        <button><i className="fas fa-plus"></i> Add User</button>
      </div>
      <table className="app-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>User Type</th>
            <th>Phone Number</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {userList.map((user) => (
            <tr key={user.userId}>
              <td>{user.userId}</td>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td>{user.email}</td>
              <td>{user.userType}</td>
              <td>{user.phoneNumber}</td>
              <td>
                <button className="edit-btn" onClick={() =>navigateToUserProfile(user.userId)}><i className="fas fa-edit"></i></button>
                <button className="delete-btn" onClick={() => deleteUser(user.userId)}><i className="fas fa-trash"></i></button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserList;
