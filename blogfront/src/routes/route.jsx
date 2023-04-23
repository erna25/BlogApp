import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Routes, Navigate, Link } from 'react-router-dom';
import Blog from '../containers/blog/blog.jsx';
import Comments from '../containers/comments/comments.jsx';
import NewPost from '../containers/newPost/newPost.jsx';
import TagsReportViewer from '../containers/report/tagReport.js'
import PostsReportViewer from '../containers/report/postReport.js'

export default class Routing extends React.Component {
    
    render() {
        return (
            <main>
                <Routes>
                    <Route path="/blog/new" element={<NewPost />} />
                    <Route path="/blog/post" element={<Comments />} />
                    <Route path="/tagsReport" element={<TagsReportViewer />} />
                    <Route path="/postsReport" element={<PostsReportViewer />} />
                    <Route path="/" element={<Blog />} />
                    <Route path="*" element={<Blog />} />
                </Routes>
            </main>
        );
    }
};
