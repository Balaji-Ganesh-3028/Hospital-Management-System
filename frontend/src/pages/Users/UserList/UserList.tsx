import React from 'react';
import './UserList.css';

const UserList: React.FC = () => {
  const users = [
    { id: 1, firstName: 'John', lastName: 'Doe', email: 'john.doe@example.com', role: 'Admin' },
    { id: 2, firstName: 'Jane', lastName: 'Smith', email: 'jane.smith@example.com', role: 'Doctor' },
    { id: 3, firstName: 'Peter', lastName: 'Jones', email: 'peter.jones@example.com', role: 'Patient' },
  ];

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
            <th>Role</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td>{user.email}</td>
              <td>{user.role}</td>
              <td>
                <button className="edit-btn"><i className="fas fa-edit"></i></button>
                <button className="delete-btn"><i className="fas fa-trash"></i></button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserList;
