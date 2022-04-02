import React, { Component } from 'react';
import { Helmet } from 'react-helmet';


export class Home extends Component {
  static displayName = Home.name;

  render () {
      return (
      <div>
            <h1 className="title">Welcome in Commento</h1>
            <Helmet>
                <script defer src="https://cdn.commento.io/js/commento.js"></script>
            </Helmet>
            <div id="commento"></div>
      </div>
      );
      
    }
}
