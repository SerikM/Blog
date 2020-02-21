import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import About from './components/About';
import BlogPosts from './components/BlogPosts';
import Projects from './components/Projects';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/about' component={About} />
        <Route path='/projects' component={Projects} />
        <Route path='/blogs/:currentBlogPost?' component={BlogPosts} />
    </Layout>
);
